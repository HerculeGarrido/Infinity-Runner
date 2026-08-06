# Infinity Runner

Um jogo do gênero Endless Runner desenvolvido na Unity utilizando C#.

O projeto foi desenvolvido como aplicação acadêmica e demonstra conceitos fundamentais de desenvolvimento de jogos 2D, incluindo geração procedural de cenário, gerenciamento de obstáculos, sistema de pontuação e integração com um minijogo.

---

## Tecnologias

- Unity
- C#
- Unity Physics 2D
- Animator
- AudioSource
- SceneManager
- PlayerPrefs

---

## Estrutura

Assets/

├── Animations/

├── Audio/

├── Fonts/

├── Materials/

├── Prefabs/

├── Scenes/

├── Scripts/

├── Sprites/

├── Tiles/

└── UI/

---

## Arquitetura

O projeto segue a arquitetura baseada em Componentes da Unity.

Cada GameObject possui scripts especializados responsáveis por seu comportamento.

O GameController atua como coordenador central da aplicação.

---

## Fluxo

Menu

↓

Cena Principal

↓

Inicialização

↓

Spawn

↓

Player

↓

Pontuação

↓

Game Over

↓

Menu

---

## Scripts principais

GameController

Responsável por:

- pontuação
- distância
- vidas
- spawn
- game over
- pausa
- HUD

---

PlayerController

Responsável por:

- movimentação
- pulo
- deslize
- animações

---

CoinController

Responsável pela coleta de moedas.

---

DiamondController

Responsável pela entrada no minijogo.

---

MemoryController

Controla o jogo da memória.

---

CameraShaker

Responsável pelos efeitos de impacto.

---

RepetirChao

Implementa o cenário infinito.

---

DeslocamentoBg

Executa o efeito de parallax.

---

## Como executar

1. Abra o projeto na Unity.

2. Abra a cena inicial.

3. Execute.

---

## Controles

↑

Pular

↓

Deslizar

---

## Persistência

PlayerPrefs é utilizado para armazenar:

- recorde
- distância
- estado do minijogo

---

## Melhorias Futuras

- Object Pooling
- Event System
- State Machine
- Separação do GameController
- Dependency Injection
- Sistema de Save
- Configurações de áudio
