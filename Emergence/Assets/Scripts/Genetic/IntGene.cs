using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class IntGene : IGene {
	// type gene (int, list)
    [SerializeField]
	string type = "int";
	// valeur
    [SerializeField]
	public int value;
	// range
    [SerializeField]
	int min;
    [SerializeField]
	int max;
	// % chance de mutation
    [SerializeField]
	float mutationChance;

	public IntGene( int Value, int Min, int Max, float MutationChance ){
		value = Value;
		min = Min;
		max = Max;
		mutationChance = MutationChance;
	}

	// children
	public IGene children(){
		
		return (IGene)this.intChildren();
	}
	/// <summary>
	/// Create a child with possible mutation.
	/// </summary>
	/// <returns>The children.</returns>
	public IntGene intChildren(){
		IntGene child = (IntGene)this.MemberwiseClone();
			child.Mutate();
		return child;
	}

	public void Mutate(){
		// si on a la chance de muter
		if ( UnityEngine.Random.Range(0.0F, 1.0F) < mutationChance ){
			// on mute au hasard dans l'interval
			value = UnityEngine.Random.Range(min, max+1);
		}
	}

}
