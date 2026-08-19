# Internal RCC Adapter Contract

`RccService` will implement `IRccRenderClient` only after the selected legacy
RCC build has passed isolated Windows-worker testing. It receives a trusted
`RccRenderRequest`, produces a PNG stream or a stable failure code, and has no
public HTTP listener.

It must never accept arbitrary browser-provided Lua, asset URLs, filesystem
paths, or SOAP requests.
