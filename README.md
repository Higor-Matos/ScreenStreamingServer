<p align="center">
  <img src="./Screenshot.jpeg" alt="Screenshot" width="500">
</p>

# Screen Streaming Server :computer: :tv:

> Um servidor de streaming de tela usando WebSocket em C#.

Este projeto implementa um servidor de streaming de tela que captura a tela do computador e envia as imagens em tempo real para um cliente através de uma conexão WebSocket.

## Tecnologias utilizadas

- C#
- .NET Framework
- System.Drawing
- WebSocketSharp

## Funcionalidades

- Streaming de tela em tempo real para um cliente WebSocket.
- Compressão de imagens usando codificação JPEG.
- Controle de início e parada do streaming através de abertura e fechamento da conexão WebSocket.

## Como usar

1. Clone este repositório: `git clone https://github.com/Higor-Matos/screen-streaming-server.git`
2. Abra o projeto em sua IDE preferida.
3. Compile e execute o projeto.
4. Abra um cliente WebSocket e conecte-se ao servidor usando o caminho `/screen`.
5. Desfrute do streaming de tela em tempo real!

## Página de Streaming Pronta

Se você deseja visualizar o streaming de tela em uma página web, confira o repositório [videoplayer](https://github.com/Higor-Matos/videoplayer). Ele fornece uma página de streaming pronta para receber o WebSocket do servidor e exibir o vídeo em tempo real.

## Reportagem sobre o Projeto

Caso queira assistir à reportagem sobre este projeto, confira o vídeo disponível em [YouTube](https://www.youtube.com/watch?v=dFQMuRxyf1c).

## Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir um "issue" ou enviar um "pull request" com melhorias, correções de bugs ou novos recursos.

