using UnityEngine;
using System.Collections;
using TinyRoar.Framework;

public class Initializer : MonoSingleton<Initializer>
{
    void Start () {
        MatchManager.Instance.Init();
        //PlayerManager.Instance.SpawnPlayer();
        SoundManager.Instance.Play("BGMLayer1", SoundManager.SoundType.Music, true, 1);
        SoundManager.Instance.Play("BGMLayer2", SoundManager.SoundType.Music, true, 1);

    }
}
