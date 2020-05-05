# Uni Software Keyboard Area

Android でもソフトウェアキーボードの表示領域を取得できるパッケージ

## 使用例

```cs
using UniSoftwareKeyboardArea;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public CanvasScaler m_canvasScaler;
    public RectTransform m_rectTransform;

    private void Update()
    {
        var rate = m_canvasScaler.referenceResolution.y / Screen.height;
        var pos = m_rectTransform.anchoredPosition;
        pos.y = SoftwareKeyboardArea.GetHeight( true ) * rate;
        m_rectTransform.anchoredPosition = pos;
    }

    private void OnGUI()
    {
        GUILayout.Label( SoftwareKeyboardArea.GetHeight( true ).ToString() );
    }
}
```

![Image (18)](https://user-images.githubusercontent.com/6134875/81075696-a56a7200-8f25-11ea-8c95-d91cc3af8cb9.gif)

## 謝辞

* このリポジトリは下記のサイト様を参考にさせていただいております  
    * https://forum.unity.com/threads/keyboard-height.291038/  