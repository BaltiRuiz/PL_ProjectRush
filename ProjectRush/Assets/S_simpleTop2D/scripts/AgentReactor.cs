
using UnityEngine;

public class AgentReactor : MonoBehaviour {

    public int reactorType;

    private float life;

    void Start()
    {
        if (reactorType == 0)
            life = 1;

        else if (reactorType == 1)
            life = 30;
    }

    void OnTriggerEnter(Collider other)
    {
        if (reactorType == 0 && other.gameObject.name.Contains("agent"))
            life = 0;
    }

    void OnTriggerStay(Collider other)
    {
        if (reactorType == 1 && other.gameObject.name.Contains("agent") && life > 0)
            life -= 0.1f;
    }

    public float getLife()
    {
        return this.life;
    }
}