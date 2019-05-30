using System;

[Serializable]
public class GlobalSave
{
    public int maxLvl;

    public GlobalSave()
    {
        maxLvl = GlobalInfo.MaxLvl;
    }
}
