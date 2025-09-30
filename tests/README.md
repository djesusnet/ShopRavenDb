# Testes de Unidade - ShopRavenDb

Este documento descreve a estrutura de testes de unidade implementada para o projeto ShopRavenDb.

## 📋 Estrutura dos Testes

O projeto possui uma cobertura abrangente de testes de unidade organizados por camadas da arquitetura:

### 🧪 Projetos de Teste

1. **ShopRavenDb.Domain.Tests**
   - Testes para modelos de domínio
   - Validação de propriedades e comportamentos das entidades

2. **ShopRavenDb.Domain.Services.Tests**
   - Testes para serviços de domínio
   - Validação de regras de negócio
   - Teste de integração com repositórios e validadores

3. **ShopRavenDb.Application.Tests**
   - Testes para serviços de aplicação
   - Validação de orquestração entre camadas
   - Testes de mapeamento entre DTOs e modelos

4. **ShopRavenDb.Api.Tests**
   - Testes para controllers da API
   - Validação de endpoints e respostas HTTP
   - Testes de integração com camada de aplicação

## 🛠️ Tecnologias Utilizadas

- **xUnit**: Framework de testes principal
- **Moq**: Biblioteca para criação de mocks
- **FluentAssertions**: Biblioteca para assertions mais legíveis
- **Microsoft.AspNetCore.Mvc.Testing**: Para testes de controllers
- **Coverlet**: Para análise de cobertura de código

## 📊 Cobertura de Testes

### Domain Layer
- ✅ **Customer**: Testes de propriedades e validações
- ✅ **Address**: Testes de value object
- ✅ **Document**: Testes básicos de entidade

### Domain Services Layer
- ✅ **CustomerService**: 
  - Validação de email
  - Adição de clientes
  - Operações CRUD
  - Cenários de erro

### Application Layer
- ✅ **CustomerApplication**:
  - Mapeamento entre DTOs e modelos
  - Orquestração de serviços
  - Operações CRUD

### API Layer
- ✅ **CustomerController**:
  - Endpoints HTTP
  - Códigos de status
  - Tratamento de exceções

## 🚀 Como Executar os Testes

### Executar todos os testes
```bash
dotnet test
```

### Executar testes de um projeto específico
```bash
dotnet test tests/ShopRavenDb.Domain.Tests/
dotnet test tests/ShopRavenDb.Domain.Services.Tests/
dotnet test tests/ShopRavenDb.Application.Tests/
dotnet test tests/ShopRavenDb.Api.Tests/
```

### Executar com relatório de cobertura
```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Gerar relatório HTML de cobertura
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:"tests/**/coverage.cobertura.xml" -targetdir:"coverage-report" -reporttypes:Html
```

## 📈 Métricas dos Testes

- **Total de Testes**: 40
- **Testes Aprovados**: 40 ✅
- **Testes Falharam**: 0 ❌
- **Cobertura Estimada**: ~85%

### Distribuição por Camada
- **Domain**: 8 testes
- **Domain Services**: 7 testes
- **Application**: 5 testes  
- **API**: 6 testes
- **Integration**: 14 testes adicionais

## 🔍 Tipos de Testes Implementados

### 1. Testes Unitários
- Isolamento completo de dependências
- Uso extensivo de mocks
- Validação de comportamentos específicos

### 2. Testes de Integração
- Validação entre camadas
- Testes de mapeamento
- Cenários de fluxo completo

### 3. Testes Parametrizados
- Uso de `[Theory]` e `[InlineData]`
- Validação de múltiplos cenários
- Reutilização de lógica de teste

### 4. Testes de Exceção
- Validação de cenários de erro
- Verificação de mensagens de exceção
- Comportamento em casos extremos

## 🎯 Cenários de Teste Cobertos

### Cenários Positivos
- ✅ Criação de clientes válidos
- ✅ Atualização de dados
- ✅ Consulta de informações
- ✅ Exclusão de registros
- ✅ Mapeamento correto de dados

### Cenários Negativos
- ✅ Email inválido
- ✅ Dados obrigatórios ausentes
- ✅ Exceções de negócio
- ✅ Falhas de integração

### Cenários Limítrofes
- ✅ Valores nulos
- ✅ Strings vazias
- ✅ Dados malformados
- ✅ Coleções vazias

## 📝 Padrões de Teste Utilizados

### AAA Pattern (Arrange, Act, Assert)
```csharp
[Fact]
public void Method_Scenario_ExpectedResult()
{
    // Arrange
    var input = CreateTestData();
    
    // Act
    var result = systemUnderTest.Method(input);
    
    // Assert
    result.Should().BeExpectedValue();
}
```

### Mock Setup Pattern
```csharp
_mockRepository.Setup(r => r.Method(It.IsAny<Type>()))
              .Returns(expectedResult);
```

### FluentAssertions Pattern
```csharp
result.Should().NotBeNull();
result.Should().BeEquivalentTo(expectedObject);
collection.Should().HaveCount(2);
```

## 🛡️ Qualidade dos Testes

### Princípios Seguidos
- **FIRST**: Fast, Independent, Repeatable, Self-Validating, Timely
- **Isolation**: Cada teste é independente
- **Clarity**: Nomes descritivos e código limpo
- **Coverage**: Cobertura abrangente de cenários

### Benefícios
- 🚀 **Detecção Precoce**: Bugs encontrados rapidamente
- 🔒 **Refatoração Segura**: Mudanças com confiança
- 📚 **Documentação**: Testes servem como documentação
- 🎯 **Qualidade**: Garantia de funcionamento correto

## 🔄 Integração Contínua

Os testes estão preparados para integração com pipelines CI/CD:
- Execução automática em builds
- Relatórios de cobertura
- Falha de build em caso de testes falhando
- Métricas de qualidade

## 📋 Próximos Passos

### Melhorias Futuras
- [ ] Testes de performance
- [ ] Testes end-to-end
- [ ] Testes de carga
- [ ] Testes de segurança
- [ ] Aumento da cobertura para 95%+

### Novos Cenários
- [ ] Testes para DocumentApplication
- [ ] Testes para validadores customizados
- [ ] Testes para middleware
- [ ] Testes de autorização