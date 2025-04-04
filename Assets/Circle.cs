using UnityEngine;

public class Circle : Shape
{
    public override void Start()
    {
        base.Start();
        
        Debug.Log("나는 도형 상속받은 자식이다.");
    }

}
