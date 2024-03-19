using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AdventurerClass
{
	Warrior,
	Mage,
	Thief,
	Archer
}