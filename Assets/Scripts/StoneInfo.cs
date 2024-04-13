public enum StoneType {
    None,
    Soil,
    Water,
    Air,
    Fire,
    Wood,
    Bone,
    Berry
}

public class StoneInfo{
    public StoneInfo(StoneType type) {
        this.type = type;
    }

    public StoneType type;

}
