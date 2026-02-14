using Terraria;
using TerrariaApi.Server;
using Mount = On.Terraria.Mount;

namespace FixJoustingLance;

[ApiVersion(2, 1)]
// ReSharper disable once UnusedType.Global
public class Plugin(Main game) : TerrariaPlugin(game)
{
    public override string Name => "FixJoustingLance";
    public override string Author => "Cai";
    public override string Description => "修复骑枪PVP报错不扣血 (PE)";
    public override Version Version => new(1, 1);

    public override void Initialize()
    {
        Mount.TryDismount += MountOnTryDismount;
    }
    

    private static bool MountOnTryDismount(Mount.orig_TryDismount orig, Terraria.Mount self, Player mountedPlayer)
    {
        return self.Type != -1 && orig(self, mountedPlayer);
    }
    


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Mount.TryDismount -= MountOnTryDismount;
        }
    }
}