public class RotateFaster : Rotate
{
    public float spinMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        spinSpeed *= spinMultiplier;
    }
}