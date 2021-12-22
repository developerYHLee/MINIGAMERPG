using UnityEngine;
using UnityEngine.UI;

public class Button_Manager : MonoBehaviour
{
    public GameObject clicked, imformation;
    public Sprite[] K_Building_Image;
    public Text name_Clicked, imformation_Next, imformation_Text;
    bool toggle, option_toggle = true, clicked_imformation_Next;
    GameObject field, menu;
    Slider slider;
    static float Button_Manager_score = 0;
    float plus = 100.0f / 14;
    bool[] toggle_button = new bool[14]; //�ʱⰪ false
    int index;

    private void Start()
    {
        field = GameObject.Find("Option").transform.Find("InputField").gameObject;
        menu = GameObject.Find("Option").transform.Find("Menu").gameObject;
        slider = GameObject.Find("Slider").GetComponent<Slider>();

        if (Button_Manager_score == 0) slider.transform.Find("Fill Area").gameObject.SetActive(false);
        else slider.value = Button_Manager_score;
    }
    public void Menu()
    {
        toggle = field.activeSelf;
        field.SetActive(!field.activeSelf);

        if (!toggle) menu.SetActive(false);
        else menu.SetActive(true);

        toggle = !toggle;
    }
    public void Option()
    {
        if (option_toggle)
        {
            menu.SetActive(true);
        }
        else
        {
            menu.SetActive(false);
            field.SetActive(false);
        }
        option_toggle = !option_toggle;
    }
    public void K_Building(int i)
    {
        index = i;

        if (!toggle_button[index])
        {
            slider.transform.Find("Fill Area").gameObject.SetActive(true);
            slider.value += plus;
            Button_Manager_score += plus;
            toggle_button[index] = true;
        }

        clicked.SetActive(true);
        clicked.GetComponent<Image>().sprite = K_Building_Image[index];
        //0:���Ұ� 1:�λ�� 2:����� 3:������ 4:�߾ӵ����� 5:���� 6:�Ļ���
        //7:��õ�� 8:�̰��� 9:õ���� 10:������ 11:�¸��� 12:���� 13:�����
        switch (index)
        {
            case 0 :
                name_Clicked.text = "���Ұ� ����";
                break;
            case 1:
                name_Clicked.text = "�λ�� ����";
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
        }
    }
    public void Clicked_Imformation()
    {
        clicked.SetActive(false);
        imformation.SetActive(true);
        Write_Text();
    }
    public void Write_Text()
    {
        if(!clicked_imformation_Next)
        {
            imformation_Next.text = ">";
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    imformation_Text.text = "�ι���ȸ��\n�������մ��� �濵��������\n\n����ϴ� �а�\n" +
                        "��ȸ�����к� ���������������к� �۷ι� �濵�к� ���������а� ���������а�";
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
            }
        }
        else
        {
            imformation_Next.text = "<";
            switch (index)
            {
                case 0:
                    break;
                case 1:
                    imformation_Text.text = "���л�ȸ��, ������ �л�ȸ��, �ε���Ǽ��к� �л�ȸ��, �ǹ�����а� �л�ȸ��," + 
                        "���������а� �л�ȸ��, �۷ι� �濵�к� �л�ȸ��, ���� �����а� �л�ȸ�� ���� �н� ��������," + 
                        "���� ��â�� ����, ���к�����, ���Ƹ� ����ȸ ȸ�ǽ�, ������/����, �߾� ���Ƹ����� ���Ƹ� ���� ��ġ�� �ִ�.";
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
            }
        }

        clicked_imformation_Next = !clicked_imformation_Next;
    }
    public void Quit_Imformation()
    {
        clicked_imformation_Next = false;
        imformation_Text.text = "";
        imformation.SetActive(false);
    }
}