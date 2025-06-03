# Desafio Técnico - Plataforma de Monitoramento de Máquinas

## Tecnologias Utilizadas
- **Backend:** .NET 5 (C#)
- **Frontend:** Angular 12
- **Node.js:** Versão 18
- **WebSocket:** Comunicação em tempo real
- **HTTP Client:** Angular HttpClient para consumo da API
- **Docker:** (Opcional) para containerização da aplicação

## Funcionalidades

### Backend (.NET 5)
- API RESTful para cadastro, atualização e consulta de máquinas.
- Cada máquina possui:
  - Identificador único (UUID)
  - Nome
  - Localização (coordenadas ou nome do local)
  - Status (operando, parada, manutenção)
- Endpoint para atualização de telemetria (localização e status).
- Atualização de status em tempo real via WebSocket.
- Simulação automática de dados de telemetria para máquinas.

### Frontend (Angular 12)
- Dashboard com lista de máquinas e status em tempo real via WebSocket.
- Formulário para cadastro e edição de máquinas com validação.
- Página de detalhes para visualizar informações completas da máquina.
- Navegação entre páginas configurada via Angular Router.

## Como Rodar o Projeto

### Pré-requisitos
- .NET 5 SDK instalado
- Node.js versão 18 instalada
- Angular CLI (opcional para desenvolvimento)
- Docker (opcional para containerização)

### Backend
1. Navegue até a pasta do backend.
2. Execute `dotnet run` para iniciar a API.

### Frontend
1. Navegue até a pasta do frontend.
2. Execute `npm install` para instalar dependências.
3. Execute `npm start` para iniciar o servidor Angular.
4. Acesse `http://localhost:4200` no navegador.

### Observação
- O comando para iniciar o frontend é `npm start` e não `ng serve`.

## Testes

- Atualmente, não há testes automatizados implementados.
- Futuras versões poderão incluir testes unitários e e2e.

## Melhorias Futuras

- Implementar testes unitários e de integração.
- Adicionar mapa interativo para visualização das máquinas.
- Containerizar a aplicação com Docker e docker-compose.
- Implementar autenticação e autorização.
- Melhorar o tratamento de erros e UX.

## Contato

Para dúvidas ou contribuições, entre em contato:  
**Guilherme Luiz Pinheiro Costa**  
Email: luizguipinh@gmail.com  
GitHub: https://github.com/guilherme-luiz-pinheiro

---

