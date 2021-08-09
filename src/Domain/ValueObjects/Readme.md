Use this to define complex value types, i.e. phone numbers. These will include hidden meaning that can be extracted, or static values that can be used to instantiate the objects (as in the case with color)

```
public class PhoneNumber
{
    public enum Extension { get; set; }
    public string Number { get; set; }
}
```