public enum StoneType {
    None,
    Soil,
    Tree,
    Water,
    Air,
    Fire,
    Bone,
    Berry
}

public class StoneInfo{
    public StoneInfo(StoneType type) {
        this.type = type;
    }

    public StoneType type;

}
