using System.Collections;
using System.Collections.Generic;
using TinyRoar.Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIHandling : MonoSingleton<UIHandling> {

    public Text EndText;
    private string _endTextOriginal;

    public void DoEndUI()
    {
        if (_endTextOriginal == null)
            _endTextOriginal = EndText.text;

        float time = GameplayTimer.Instance.GetTime();
        EndText.text = _endTextOriginal.Replace("XXX", time.ToString("n2"));
    }

}
