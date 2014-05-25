// Thruster C# Script (version: 1.02)
// SPACE UNITY - Space Scene Construction Kit
// http://www.spaceunity.com
// (c) 2013 Stefan Persson

// DESCRIPTION:
// This script controls the thruster used for spaceship propulsion.

// INSTRUCTIONS:
// Use the thruster prefab which has the required particle system this script depends upon.
// Configure the thruster force and other parameters as desired.

// PARAMETERS:
//   thrusterForce		(the thruster force to be applied to it's rigidbody parent when active
//   addForceAtPosition	(whether or not to introduce torque/rotation... use with care as it is super sensitive for positioning)
//   soundEffectVolume	(sound effect volume of thruster)

// Version History
// 1.02 - Prefixed with SU_Thruster to avoid naming conflicts.
// 1.01 - Initial Release.

using UnityEngine;
using System.Collections;

public class Thruster : MonoBehaviour {

	private ParticleSystem cacheParticleSystem;
	public float parentSpeed;

	public void StartThruster() {
	}
	
	public void StopThruster() {
	}
	
	void Start () {
		parentSpeed = 0;
		cacheParticleSystem = particleSystem;
		if (cacheParticleSystem == null) {
		}		
	}	
	
	void Update () {
		if(parentSpeed <= 3){
			cacheParticleSystem.startLifetime = 0.08f;
		} 

		if(parentSpeed <= 4 && parentSpeed > 3){
			cacheParticleSystem.startLifetime = 0.2f;
		} 

		if(parentSpeed == 0){
			cacheParticleSystem.maxParticles = 0;
		} else {
			cacheParticleSystem.maxParticles = 200;
		}
	}
	
	void FixedUpdate() {
	}
}
