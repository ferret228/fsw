using System;

public class EngineModule : AbstractModule {
	public EngineModule(String name, String description, Module module, float m1, float m2) {
		base.moduleName = "engineModule1";
		base.moduleDescription = "coolDescription";
		base.moduleType = Module.engine;
		base.moduleMultiplyer1 = 30;
		base.moduleMultiplyer2 = 30;
	}

	public float GetNewSpeed(float speed) {
		return base.ApplyModuleMultiplyer1(speed);
	}

	public float GetNewRotateSpeed(float speed) {
		return base.ApplyModuleMultiplyer2(speed);
	}
}