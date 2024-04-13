public enum StoneType {
    None,
    Soil,
    Water,
    Air,
    Fire
}

public class StoneInfo{
    public StoneInfo(StoneType type) {
        this.type = type;
    }

    public StoneType type;

}
