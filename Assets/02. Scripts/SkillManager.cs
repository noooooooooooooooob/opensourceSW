using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject skill3Prefab;  // ��ȭ ���� ������
    public GameObject skill4Prefab;  // ���� ���� ������

    private bool isSkill3Selected = false;
    private bool isSkill4Selected = false;

    void Update()
    {
        // Ű �Է����� ��ų ����
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isSkill3Selected = true;
            isSkill4Selected = false;
            Debug.Log("��ų 3 ���õ�: ��Ŭ������ ��ġ ����");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            isSkill3Selected = false;
            isSkill4Selected = true;
            Debug.Log("��ų 4 ���õ�: ��Ŭ������ ��ġ ����");
        }

        // ���콺 ��Ŭ������ ���� ��ġ ����
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 spawnPos = GetMouseWorldPosition();

            if (isSkill3Selected)
            {
                Instantiate(skill3Prefab, spawnPos, Quaternion.identity);
                isSkill3Selected = false;
            }
            else if (isSkill4Selected)
            {
                Instantiate(skill4Prefab, spawnPos, Quaternion.identity);
                isSkill4Selected = false;
            }
        }
    }

    // ���콺 ��ǥ �� ���� ��ǥ ��ȯ (2D ����)
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;
        return worldPos;
    }
}

