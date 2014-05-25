using System;

public abstract class AbstractModule {
	public string moduleName;
	public string moduleDescription;
	public Module moduleType;
	public float moduleMultiplyer1;
	public float moduleMultiplyer2;

	public float ApplyModuleMultiplyer1(float baseParam) {
		float result = baseParam / 100 * moduleMultiplyer1;
		return baseParam + result;
	}

	public float ApplyModuleMultiplyer2(float baseParam) {
		float result = baseParam / 100 * moduleMultiplyer2;
		return baseParam + result;
	}
}