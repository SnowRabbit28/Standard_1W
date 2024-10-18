using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Rocket : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreTxt;
    [SerializeField] private TextMeshProUGUI HighScoreTxt;
    [SerializeField] private Image fuelBar;

    private Rigidbody2D rb;
    
    private float fuel = 100f;
    private float maxFuel = 100f; // 최대 연료

    private readonly float SPEED = 5f;
    private readonly float FUELPERSHOOT = 10f;

    private float highestScore = 0f; // 최고거리 변수 ☆
    private float currentScore = 0f; // 현재거리 변수 ☆



    void Awake()
    {
        // TODO : Rigidbody2D 컴포넌트를 가져옴(캐싱) 
        rb = GetComponent<Rigidbody2D>(); // 아..이게 캐싱이구나 + 이렇게 get을 통하면 드래그 안해도 된다!!!!!
        //ShootBtn = GetComponent<Button>();
        fuelBar = GetComponentInChildren<Image>();
        UpdateFuelBar(); // 연료바 업데이트
    }

    public void Update()
    {
        // 매 프레임마다 0.1의 연료를 추가
        AddFuel(0.1f);
        UpdateFuelBar(); // 연료바 업데이트

        // 현재 로켓의 y 위치를 기준으로 현재 거리 갱신 ☆
        currentScore = transform.position.y;

        // 현재 거리보다 최고거리가 낮으면 갱신
        if (currentScore > highestScore)
        {
            highestScore = currentScore;
        }

        // 텍스트에 현재 거리와 최고 거리 표시
        currentScoreTxt.text = $"{currentScore:F2} M";
        HighScoreTxt.text = $"{highestScore:F2} M";
    }

    public void Shoot()
    {
        // TODO : fuel이 넉넉하면 윗 방향으로 SPEED만큼의 힘으로 점프, 모자라면 무시

        if (fuel > 0f)
        {
            rb.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
            fuel -= FUELPERSHOOT;
            UpdateFuelBar();
        }
        else { Debug.Log("연료부족"); }
    }

    public void OnReBtn()
    {
        SceneManager.LoadScene("RocketLauncher");
    }
    private void AddFuel(float amount)
    {
        fuel = Mathf.Min(fuel + amount, maxFuel); // 최대 연료 이상으로 증가하지 않도록 제한
    }
    private void UpdateFuelBar()
    {
        // 연료 비율을 계산하고, fuelBar의 fillAmount에 반영
        float fuelRatio = fuel / maxFuel;
        fuelBar.fillAmount = fuelRatio; // 연료비율에 따라 연료바 크기 변경
    }


}
