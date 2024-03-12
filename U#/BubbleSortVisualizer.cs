using UdonSharp;
using UnityEngine;

public class BubbleSortVisualizer : UdonSharpBehaviour
{
    public GameObject elementPrefab;
    public int numElements = 10;
    public float maxElementHeight = 10.0f;
    public float sortingSpeed = 1.0f;
    public float elementSpacing = 1.5f; // 要素の間隔

    private Transform[] elements;
    private bool isSorting = false;

    private int i = 0; // 外側のループのインデックス
    private int j = 0; // 内側のループのインデックス
    private bool swapped = false; // 要素が交換されたかのフラグ
    private float startTime = 0.0f; // ソート処理の開始時間

    private void Start()
    {
        GenerateElements();
    }

    private void GenerateElements()
    {
        elements = new Transform[numElements];

        for (int i = 0; i < numElements; i++)
        {
            GameObject elementObject = Instantiate(elementPrefab);
            Transform elementTransform = elementObject.transform;

            elementTransform.position = new Vector3(i * elementSpacing, 0, 0); // 要素の間隔を考慮
            float randomHeight = Random.Range(0f, maxElementHeight);
            elementTransform.localScale = new Vector3(1, randomHeight, 1);

            elements[i] = elementTransform;
        }
    }

    public override void Interact()
    {
        StartSorting();
    }

    public void StartSorting()
    {
        if (!isSorting)
        {
            isSorting = true;
            i = 0; // 外側のループを初期化
            j = 0; // 内側のループを初期化
            swapped = false; // フラグを初期化
            startTime = Time.realtimeSinceStartup; // 開始時間を記録
        }
    }

    private void Update()
    {
        if (isSorting)
        {
            // ソート処理
            if (i < elements.Length - 1)
            {
                if (j < elements.Length - i - 1)
                {
                    // 要素の比較と交換
                    if (elements[j].localScale.y > elements[j + 1].localScale.y)
                    {
                        Vector3 tempScale = elements[j].localScale;
                        elements[j].localScale = elements[j + 1].localScale;
                        elements[j + 1].localScale = tempScale;
                        swapped = true;
                    }

                    j++;
                }
                else
                {
                    // 内側のループが終了した場合
                    if (!swapped)
                    {
                        // ソートが完了した場合
                        isSorting = false;
                    }
                    else
                    {
                        // 次の外側のループへ
                        i++;
                        j = 0;
                        swapped = false;
                        startTime = Time.realtimeSinceStartup;
                    }
                }
            }
            else
            {
                // 外側のループが終了した場合
                isSorting = false;
            }
        }
    }
}
