using UnityEngine;
using System.Collections;


public class IntGene : IGene {
	// type gene (int, list)
	string type = "int";
	// valeur
	public int value;
	// range
	int min;
	int max;
	// % chance de mutation
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
	/// Create a child with possible mutation the children.
	/// </summary>
	/// <returns>The children.</returns>
	public IntGene intChildren(){
		IntGene child = (IntGene)this.MemberwiseClone();
		// si on a la chance de muter
		if ( Random.Range(0.0F, 1.0F) < mutationChance ){
			// on mute au hasard dans l'interval
			child.value = Random.Range(min, max+1);
		}
		return child;
	}

}
