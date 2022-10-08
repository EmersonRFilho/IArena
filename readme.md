# IArena

## Resumo:
Este projeto tem o propósito de servir como uma ferramenta para facilitar o ensino de lógica de programação e inteligência artificial voltada para o desenvolvimento de jogos como árvores de decisão, máquina de estados, fuzzy, etc.

O objetivo do aluno nesta primeira versão do projeto é cumprir os desafios propostos por cada fase disponível dentro do projeto. O aluno deverá desenvolver um script que será inserido dentro de um personagem já posicionado dentro da cena para que ele cumpra esse desafio por conta própria. O aluno poderá desenvolver um único script para resolver todas as fases, ou um script para cada fase, o que achar mais confortável.

## Regras:
### Nomenclatura:
O script desenvolvido deve ser nomeado usando os seguintes padrões:
- ``NomeDoAlunoIA`` para scripts gerais;
- ``NomeDoAlunoIA1`` para scripts específicos para uma cena, neste caso "1" se refere à cena "Challenge 1".

### Regras Gerais:
O aluno pode implementar a lógica de seu script do jeito que quiser desde que siga algumas regras
1. Não é permitido o uso de outras biliotecas internas além da biblioteca Movement. São essas:
   1. ``Core``;
   2. ``Managers``;
   3. ``Commands``;
   4. ``LevelElements``.
2. Não é permitido o uso do método ``SendMessage()`` em qualquer lugar do código do aluno;
3. Além do script desenvolvido pelo aluno, nenhum componente pode ser adicionado ou alterado no objeto do personagem;
4. Pode ser criado um script de lógica do zero desde que siga a estrutura do script de exemplo disponível no projeto;
5. Após o desenvolvimento do(s) script(s), este(s) devem ser enviados ao instrutor diretamente. Não sendo permitido qualquer alteração no repositório o qual o projeto foi disponibilizado.

## Documentação:
Seguindo as regras de desenvolvimento do padrão estrutural de Faxada, o Script herdará uma classe chamada ``BrainBase``, que contém funções de fácil uso para a ativação das ações que o personagem pode executar durante um desafio, além disso o aluno terá acesso à criação de classes do tipo ``Steering`` que podem alterar a movimentação de seu personagem de várias maneiras. Todos os métodos e classes estão documentadas dentro do código.
### BrainBase
>Contém todos os métodos e atributos que um personagem irá precisar para funcionar dentro do ambiente do jogo.
<details>

#### Métodos:
|Nome|Decrição|Parâmetros|
|---|---|---|
|``GetVision()``|Popula uma lista chamada ObjectsInRange com objetos detectáveis no alcance da visão do personagem|-|
|``SetMovementBehaviours()``|Altera o comportamento de movimentação do personagem de acordo com os Behaviours passados para a função|`Steering[]` behaviours|
|``Collect()``|Coleta um objeto se estiver encostando nele e o guarda em algum lugar dependendo do tipo de objeto coletado|`Collectable` item|
|``EatFood()``|Consome um objeto de comida, recuperando vida e fome de acordo com a comida consumida|``Food`` food|
|``Attack()``|Ataca um oponente dentro do alcance da arma do personagem com dano baseado na mesma|``CharacterBehaviours`` target|

#### Atributos:
|Nome|Tipo|Descrição|
|---|---|---|
|``objectsInRange``|`List<Transform>`|Lista com todos os `Transform`s detectáveis dentro da visão do personagem|
|``chara``|`CharacterBehaviours`|Referência ao próprio personagem base, é possível acessar alguns atributos próprios do personagem por aqui|
</details>

### Steering:
>Todos os métodos que afetam a movimentação do personagem são do tipo `Steering` e podem ser criados e passados para o método `SetMovementBehaviours()` como por exemplo: `new MovementBehaviour();`
<details>

#### Classes:
|Nome|Descrição|Parâmetros|
|---|---|---|
|`SeekBehaviour()`|Anda em direção à posição de um alvo.|`Transform` target, `float` weight|
|`Flee()`|Anda na direção contrária à posição de um alvo.|`Transform` target, `float` weight|
|`PursueBehaviour()`|Anda em direção à posição de um alvo tentando interceptá-lo em seu caminho.|`Transform` target, `float` weight|
|`StraightLineBehaviour()`|Anda indefinidamente em uma direção.|`Float` angle, `float` weight|
|`WanderBehaviour()`|Anda em direções pseudo aleatórias, como se estivesse vagando.|`float` wanderRate, `float` wanderOffset, `float` wanderRadius, `float` weight|
</details>

### Chara
>É o objeto base de todos os personagens, é possível conferir algumas coisas dentro dessa classe à partir da classe `CharacterBehaviours`, além de acessar seus atributos e listas.
<details>

#### Atributos:
|Nome|Descrição|Tipo|
|---|---|---|
|``IsDead``|Se o personagem está morto.|`bool`|
|``Score``|Qual o placar atual do personagem.|`int`|
|`FoodBag`|A lista com as comidas coletadas pelo personagem|`List<Food>`|
|``Backpack``|A lista com os itens gerais coletados pelo personagem|`List<Collectable>`|
|`Weapon`|A arma que o personagem está empunhando|`Weapon`|
</destails>