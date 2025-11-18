# BloodBank – Visão Geral, Arquitetura e Riscos

## Visão Geral do Sistema
- **Contexto**: solução ASP.NET Core para gerenciar doadores, doações e estoque de sangue. APIs REST expostas em `BloodBank.API`, lógica de negócios encapsulada em `BloodBank.Application`, entidades/regras de domínio em `BloodBank.Core` e acesso a dados via Entity Framework Core em `BloodBank.Infrastructure`.
- **Tecnologias-chave**: ASP.NET Core 7+, MediatR para Command/Query, EF Core com SQL Server (ver `BloodBank.Infrastructure/Persistence/BloodBankDbContext.cs`), testes com xUnit em `BloodBank.Tests`.
- **Padrão geral**: abordagem baseada em CQRS simples (Commands e Queries), com uso de `ResultViewModel` para padronizar respostas de sucesso/erro.

## Arquitetura em Camadas
- **API (`BloodBank.API`)**: Controllers (`DoadoresController`, `DoacoesController`, `EstoqueController`) recebem requisições HTTP e delegam a comandos/consultas via `IMediator`. `Program.cs` registra `AddApplication()` e `AddInfrastrucure()` e habilita Swagger em desenvolvimento.
- **Application (`BloodBank.Application`)**: contém comandos e queries (ex.: `InsertDoadorCommand`, `GetDoacoesAllQuery`) e respectivos handlers. Regras de negócio, validações e mapeamentos para view models ficam aqui.
- **Core (`BloodBank.Core`)**: define entidades (`Doador`, `Doacao`, `Estoque`, `Endereco`), enums de tipo sanguíneo e fator RH, além dos contratos de repositório (`IDoadorRepository`, `IDoacaoRepository`).
- **Infrastructure (`BloodBank.Infrastructure`)**: implementa repositórios com EF Core (`DoadorRepository`, `DoacaoRepository`), configura o `BloodBankDbContext` e faz o wiring de dependências em `InfrastrucureModule`.
- **Tests (`BloodBank.Tests`)**: contém suites para commands e queries, garantindo que os handlers retornem `ResultViewModel` coerentes.

### Fluxo de Requisição (exemplo)
1. `POST /api/Doacoes` (`DoacoesController.Post`) recebe um `InsertDoacaoCommand`.
2. O controller envia o comando via MediatR; `InsertDoacaoHandler` consulta `IDoadorRepository` e `IDoacaoRepository`.
3. Após validações (idade, peso, intervalo por gênero, volume), o handler cria a entidade `Doacao` e chama `Insert` no repositório.
4. `DoacaoRepository` persiste via `BloodBankDbContext` e EF Core.
5. O handler devolve `ResultViewModel<int>` contendo o `Id` criado; o controller retorna HTTP 201.

## Funcionalidades Principais

### Gestão de Doadores
- **Endpoints** em `DoadoresController`: `POST` (criação), `GET /{id}` (consulta), `GET` paginado, `PUT` (atualização) e `DELETE`.
- **Validações atuais**: `InsertDoadorHandler` garante unicidade de e-mail antes de inserir. `UpdateDoadorHandler` apenas verifica existência e sobrescreve dados, sem validações adicionais; `DeleteDoadorHandler` efetua soft delete via `SetAsDeleted`.
- **Modelagem**: `Doador` agrega `Endereco` como owned type no EF Core, lista de `Doacao` e enums de sangue/RH.

### Gestão de Doações
- **Criação (`InsertDoacaoHandler`)**: aplica regras de elegibilidade – idade mínima 18 anos, peso mínimo 50kg, intervalo mínimo de 90 dias (feminino) ou 60 dias (masculino) desde a última doação e volume entre 420 ml e 470 ml.
- **Consulta**: `GetDoacaoByIdHandler` converte para `DoacaoViewModel`, enquanto `GetDoacoesAllHandler` retorna lista de `DoacoesItemViewModel`.
- **Atualização/Exclusão**: `UpdateDoacaoHandler` atualiza doador/volume e reseta a data para `DateTime.Now`; `DeleteDoacaoHandler` realiza soft delete.

### Estoque de Sangue
- **Cálculo agregado**: `GetEstoqueAllHandler` e `GetEstoqueLast30DaysHandler` combinam doações e doadores e usam `Estoque.CalcularEstoque` para agrupar volume por tipo sanguíneo e fator RH.
- **Filtros**: `/api/Estoque` retorna todo o estoque; `/api/Estoque/last-30-days` limita às doações entre hoje e os últimos 30 dias.

## Vulnerabilidades e Pontos de Atenção

1. **Ausência total de autenticação/autorização**  
   - `Program.cs` registra apenas `AddAuthorization` implícito; não há `AddAuthentication` nem policies. Todos os endpoints ficam públicos, o que é crítico, pois dados pessoais (endereços, data de nascimento, peso) são retornados sem restrição.

2. **Validação insuficiente de entrada**  
   - `InsertDoadorCommand` aceita strings e `Endereco` sem checagens de formato (e-mail, CEP) ou limites. Não há atributos DataAnnotations ou validação manual, permitindo persistir dados inconsistentes ou payloads excessivos (potencial DoS/armazenamento de scripts).
   - `UpdateDoadorHandler` não revalida e-mail duplicado, permitindo que dois usuários compartilhem o mesmo e-mail após atualização.

3. **Enumeração fácil de dados sensíveis**  
   - `DoadoresController.GetAll` permite configurar `pageSize` arbitrário (sem limites) e retorna toda a estrutura de `Doador` (inclusive endereço). Um atacante pode fazer scraping completo em poucas requisições.

4. **Paginação ineficiente e suscetível a DoS**  
   - `IDoadorRepository.GetAll` carrega todos os registros em memória (`ToListAsync`) antes de paginar. Consultas com base grande impactam memória/CPU e, combinadas com `pageSize` alto, podem derrubar o serviço.

5. **Manipulação livre de estoque e doações**  
   - Falta controle de integridade referencial em atualizações: `UpdateDoacaoCommand` não revalida elegibilidade, intervalo ou volume, permitindo ajustes indevidos no histórico/estoque.

6. **Sem auditoria ou rate limiting**  
   - Não há logging de domínio, trilhas de auditoria ou qualquer limitação de requisições. Um atacante pode tentar automações de exclusão/inserção sem ser detectado.

7. **Superfície de ataque ampliada pelo Swagger público**  
   - Swagger UI é habilitado sempre em desenvolvimento, mas não há garantias de que esteja limitado em produção. Sem autenticação, esta UI oferece documentação interativa completa para qualquer usuário.

## Recomendações Iniciais
- Implementar autenticação (JWT/OAuth2) e políticas de autorização específicas para operações críticas.
- Validar e normalizar entradas (FluentValidation/DataAnnotations) e impor limites razoáveis de `pageSize`.
- Atualizar repositórios para paginar no banco (`Skip/Take` diretamente na query) e projetar DTOs que exponham apenas os campos necessários.
- Reaproveitar regras de negócio de criação também nas atualizações e considerar versões/histórico para evitar sobrescritas silenciosas.
- Configurar rate limiting, logging estruturado e mascaramento/redação de dados sensíveis nas respostas de API.
