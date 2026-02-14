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
    public override Version Version => new(1, 0);

    public override void Initialize()
    {
        Mount.CanDismount += MountOnCanDismount;
        Mount.CanDismountWithResult += Mount_CanDismountWithResult;
    }

    private Terraria.Mount.DismountCheckResult Mount_CanDismountWithResult(Mount.orig_CanDismountWithResult orig, Terraria.Mount self, Player mountingPlayer)
    {
        if (self.Type != -1)
            return Terraria.Mount.DismountCheckResult.FailedCCed;
        return orig(self, mountingPlayer);
    }

    private static bool MountOnCanDismount(Mount.orig_CanDismount orig, Terraria.Mount self, Player mountingPlayer)
    {
        return self.Type != -1 && orig(self, mountingPlayer);
    }


    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            Mount.CanDismount -= MountOnCanDismount;
            Mount.CanDismountWithResult -= Mount_CanDismountWithResult;
        }
    }
}