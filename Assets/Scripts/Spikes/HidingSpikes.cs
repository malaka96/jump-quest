using UnityEngine;

public class HidingSpikes : MonoBehaviour
{
    [SerializeField] private float hidingValue;
    [SerializeField] private float hidingSpeed;

    private Vector2 originalPos;
    private float targetYPos;
    public bool moveDown;

    void Start()
    {
        originalPos = transform.position;
        targetYPos = originalPos.y - hidingValue;
    }


    void Update()
    {
        if(moveDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(originalPos.x, targetYPos), hidingSpeed * Time.deltaTime);
            if(transform.position.y <= targetYPos)
            {
                moveDown = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, originalPos, hidingSpeed * Time.deltaTime);
            if(transform.position.y >= originalPos.y)
            {
                moveDown = true;
            }
        }

    }
}
