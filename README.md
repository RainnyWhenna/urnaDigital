# urnaDigital

Aplicação criada para que o time da Certponto possa decidir o melhor lugar para almoçar, sem brigas e sem repetir na mesma semana.

## Documentação da API

#### Retorna os dados do funcionário conforme matrícula digitada

```http
  GET /api/Home/GetFuncionarioByMatricula
```

| Parâmetro      | Tipo  | Descrição                                   |
| :------------- | :---- | :------------------------------------------ |
| `intMatricula` | `int` | **Obrigatório**. A matrícula do funcionário |

#### Retorna os restaurantes que não foram escolhidos na semana

```http
  GET /api/Home/GetRestaurantes
```

Não precisa passar parâmetros.

#### Grava no banco de dados os votos

```http
  POST /api/Home/CreateVoto
```

| Parâmetro       | Tipo       | Descrição                                               |
| :-------------- | :--------- | :------------------------------------------------------ |
| `idVotacao`     | `int`      | **Autoincrementado no banco ao inserir um novo registro |
| :----------     | :--------- | :----------------------------------                     |
| `dtVotacao`     | `string`   | **Obrigatório**. Data do voto (sysdate)                 |
| :----------     | :--------- | :----------------------------------                     |
| `idFuncionario` | `int`      | **Obrigatório**. Id do funcionário                      |
| :----------     | :--------- | :----------------------------------                     |
| `idRestaurante` | `int`      | **Obrigatório**. Id do restaurante votado               |

#### Retorna os dados do restaurante mais votado.

```http
  GET /api/Home/GetRestauranteEscolhido
```

Não precisa passar parâmetros. Conta os votos e retorna o ganhador.

#### Limpa a coluna de data na tabela de restaurantes

```http
  POST /api/Home/ZerarVotacao
```

Não precisa passar parâmetros. Quando um restaurante é escolhido, é inserida a data da escolha na tabela de restaurantes coluna escolhidoEm, toda Sexta-feita é rodado o endpoint para zerar essa coluna e o restaurante voltar a ser exibido

## Autores

- Rainny Whenna Melo Lemos

## Funcionalidades

- Contagem de votos (Exibe o resultado entre 11:45 e 12:15)
- Permite a votação apenas até às 11:44:59.
- Exibe somente os restaurantes que ainda não foram escolhidos naquela semana## Melhorias futuras

- Exibir um cronômetro na última tela mostrando o tempo que falta para acabar a votação;
- Recarregar a tela a cada 15 segundos sem que o cliente precise dar reload;
- Exibir quantos funcionário já votaram e uma parcial dos votos;
- Tratar empate;

## Demonstração

Link com o vídeo demonstrativo do sistema

https://youtu.be/1WR2dvqapt8
