using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject fireWallPrefab;

    private bool isFireWallMode = false;
    private Vector2 firstClickPos;
    private bool waitingForSecondClick = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isFireWallMode = true;
            waitingForSecondClick = false;
            Debug.Log("FireWall 모드 ON: 첫 번째 클릭을 하세요.");
        }

        if (isFireWallMode && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (!waitingForSecondClick)
            {
                firstClickPos = mousePos;
                waitingForSecondClick = true;
                Debug.Log("첫 번째 클릭 완료: " + firstClickPos);
            }
            else
            {
                Vector2 secondClickPos = mousePos;
                CreateFireWall(firstClickPos, secondClickPos);
                isFireWallMode = false;
                waitingForSecondClick = false;
                Debug.Log("FireWall 생성 완료");
            }
        }
    }

    void CreateFireWall(Vector2 start, Vector2 end)
    {
        Vector2 midPoint = (start + end) / 2f;
        float distance = Vector2.Distance(start, end);
        Vector2 direction = end - start;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        GameObject wall = Instantiate(fireWallPrefab, midPoint, Quaternion.Euler(0, 0, angle));
        wall.transform.localScale = new Vector3(distance, wall.transform.localScale.y, wall.transform.localScale.z);
    }
}
