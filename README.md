<div align="center">
 
<h1>UniLinks</h1>

<img width="150px" src="https://i1.wp.com/codigosimples.net/wp-content/uploads/2018/01/aspcore.png?fit=280%2C280&ssl=1" alt="Logo do asp.net core">

Plataforma para visualizar e gerenciar os links das aulas virtuais gravadas.


![.NET Core](https://github.com/Speckoz/UniLinks/workflows/.NET%20Core/badge.svg)
</div>


<h2>Motivação</h2>
<p> 
Devido a pandemia do corovavirus, as faculdades adoratam as aulas online, e percebemos que algumas faculdades estavam armazenando os links das gravações em planilhas do excel, o que após um certo tempo, foi perceptível que isso não foi uma boa solução, principalmente pelo fato de ter uma grande quantidade de aulas, o que acaba dificultando tanto para o aluno quanto para o coordenador a visualização das mesmas, o que levou a um visível desinteresse nas aulas por parte dos alunos, o que pode ser confirmado pelo fato de cada vez menos alunos visualizarem as gravações.
O Aluno não pode por exemplo procurar uma aula de uma disciplina específica, é necessário procurar no calendário o dia da semana, para então, procurar a data nas planilhas. O que torna um trabalho cansativo e entediante. 
</p>

<h2>A Solução</h2>
<p>
Visando a solução desse problema, desenvolvemos uma plataforma moderna, e rápida responsável por gerenciar e disponibilizar de maneira visual e organizada as aulas gravadas, onde os alunos podem acessar suas respectivas aulas e salas online, podendo ver os assuntos, filtrar por dia da semana e disciplina, o que torna muito mais fácil e organizado o modo como se procura as aulas e salas.
Com a plataforma, fica muito mais fácil e intuitivo para o coordenador gerenciar as aulas gravadas, onde é possível atribuir disciplinas, períodos e assuntos para uma aula, o que torna possível por exemplo pesquisar pelas aulas  de um período em específico. A plataforma também foi desenhada para atender perfeitamente os usuários que acessam pelo celular (o que é uma experiência horrível utilizando excel).
</p>

<img src="https://user-images.githubusercontent.com/37851168/101137342-af984000-35dc-11eb-8155-a378b96380a4.png" alt="Pagina inicial do site">


<img src="https://user-images.githubusercontent.com/40467826/84333483-48865b00-ab66-11ea-9e73-8e4472b5127d.png">

<img src="https://user-images.githubusercontent.com/40467826/84333547-72d81880-ab66-11ea-8f40-278336597dc0.png">

<img src="https://user-images.githubusercontent.com/40467826/84333588-8f745080-ab66-11ea-8cc6-71bdbb6927bb.png">

<h2>Tecnologias Utilizadas</h2>
<ul>
    <li>ASP.NET Core 3.1 (API + MVC)</li>
    <li>MySQL</li>
    <li>Docker & Docker-Compose</li>
    <li>C# 8</li>
    <li>Nginx</li>
</ul>

<h2>Contribuições</h2>
<p>
O projeto é 100% open source e contribuições são 100% bem-vindas, bastar fazer uma PR ou Issue :)
</p>

<h2>Apresentação completa do projeto</h2>
<p>

Você pode conferir a apresentação completa do projeto <a href="https://docs.google.com/presentation/d/1duCzcA3vW1-VhRJummfUcm4Y5fP8Xc5x9KNbO67RqbA/edit?usp=sharing">aqui</a>
</p>
<p>Trello: <a href="https://trello.com/b/hJYOTHcl/unilinks" target="_blank">Ver</a></p>

<h2>Licença</h2>
<p>O projeto está licenciado sob a licença <strong>MIT.</strong> </p>


<h2>Padrões do projeto</h2>
<p>
<h4>Nomenclatura</h4>
O projeto segue o padrão de nomenclatura especificado <a href="https://github.com/Speckoz/Nomenclatura">aqui</a>

<h4>VO, Business e Repository</h4>

Organizamos o projeto em 4 camadas principais: Controller, Repository, Business e VO (value object)

<img width="500px" src="https://media.discordapp.net/attachments/553858177331101696/704487933100556288/unknown.png" alt="Padrão de projeto VO">
</p>


<h2>Rodando o projeto</h2>
<p>

<h4>Rodando em produção</h4>

Para rodar o projeto em produção, basta ter o docker-compose instalado, entrar na pasta raiz do projeto e subir os containeres rodando o script de deploy como abaixo:

```bash
root@speckoz:~$ bash deploy.sh
```

Após isso o servidor estará rodando na sua porta 80.

<small>Nota: Visando uma maior segurança, apenas o container do nginx (porta 80) está exposto</small>

<h4>Rodando em desenvolvimento</h4>

Para rodar o projeto localmente, você precisa ter instalado o .NET Core SDK 3.1 e o MySQL.
Antes de iniciar o projeto, certique-se de ter setado as variaveis de ambiente <strong>'DBHOST', 'DBPASSWORD' 'DBPORT' e 'DBUSER' </strong> no arquivo 'UniLinks.API/Properties/lanchSettings.json para que a API consiga se conectar no banco.

Após isso basta iniciar o projeto UniLinks.API e o projeto UniLinks.Client.Site e o servidor estará rodando na porta 5000

Se você estiver usando o **dotnet cli** basta rodar o comando abaixo no projeto da API e no Client.Site

```bash
root@speckoz:~$ dotnet run
```
</p>
