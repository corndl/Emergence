using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class FloatGene : IGene {
	// type gene
	[SerializeField]
	string type = "float";
	// valeur
	[SerializeField]
	public float value;
	// range
	[SerializeField]
	float min;
	[SerializeField]
	float max;
	// % chance de mutation
	[SerializeField]
	float mutationChance;
	
	public FloatGene( float Value, float Min, float Max, float MutationChance ){
		value = Value;
		min = Min;
		max = Max;
		mutationChance = MutationChance;
	}

	public FloatGene( float Min, float Max, float MutationChance ){
		value = UnityEngine.Random.Range(min, max);
		min = Min;
		max = Max;
		mutationChance = MutationChance;
	}

	// children
	public IGene children(){
		
		return (IGene)this.floatChildren();
	}
	/// <summary>
	/// Create a child with possible mutation.
	/// </summary>
	/// <returns>The children.</returns>
	public FloatGene floatChildren(){
		FloatGene child = (FloatGene)this.MemberwiseClone();
		child.Mutate();
		return child;
	}

	/// <summary>
	/// Mutate this instance.
	/// </summary>
	public void Mutate(){
		// si on a la chance de muter
		if ( UnityEngine.Random.Range(0.0F, 1.0F) < mutationChance ){
			// on mute au hasard dans l'interval
			value = UnityEngine.Random.Range(min, max);
		}
	}
	
}
