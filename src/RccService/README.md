# RCC Adapter Boundary

This project is the future internal C# adapter for the selected legacy RCCService
build. It does **not** contain or redistribute an RCC executable, archive, client
binary, or user-created content.

The adapter will later submit trusted, server-generated jobs over a private
network to an isolated Windows RCC worker. It must never expose the RCC SOAP
port to browsers or the public internet.
