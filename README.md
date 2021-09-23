# konatus_poc
POC de API Manutenção 

O repositório conta com dois projetos na pasta fontes que compreende duas soluções distintas, uma API em C# netCore e outra o front-end em angular.


[API netCore]
- SGBD: PostgreSQL 
- Database: Konatus (precisa ser criada).
- Migração:  02 opções: 
1. Pode ser gerar o Update-Database considerando os dois contextos da aplicação: ApplicationDbContext e KonatusDbContext.
2. Utilizar o script disponivel na pasta sql.

- Localhost porta 44380. (pode mudar se aberto em tempo de dev).
- AppSettings: Configurações de Auth, pastas e credenciais de banco de dados.


[Front Angular]
- Rodar o npm install no projeto (foi excluida a node_modules).
- configurar o environment.ts disponível na pasta "environments/" para responder na porta que a API esta rodando.


[Projeto em produção]

1. Clicque em registrar-se na tela de autenticação. 
2. Informe suas credencias.
3. Tente o login novamente.
4. Clique no menu Manutenções (Criar Manutenção).
5. Cada manutenção criada possui seu menu lateral que pode ser acesso pelo icone ":".
6. Ao clicar em executar os passos da stage são executados.



