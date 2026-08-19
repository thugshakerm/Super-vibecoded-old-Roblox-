namespace Roblox.Application.Servers;

public interface IVipServerLinkCodeGenerator
{
    string Generate(int length);
    string Hash(string code);
}
