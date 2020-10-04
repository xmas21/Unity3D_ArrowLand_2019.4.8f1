using UnityEngine;
using UnityEngine.UI;

public class HpMpManager : MonoBehaviour
{
    private Image hpBar;

    private void Start()
    {
        hpBar = transform.GetChild(1).GetComponent<Image>();
    }

    private void Update()
    {
        FixAngle();
    }

    private void FixAngle()
    {
        transform.eulerAngles = new Vector3(50, -180, 0);
    }

    public void UpdateHpBar(float hpcurrent,float hpMax)
    {
        hpBar.fillAmount = hpcurrent / hpMax;
    }
}
