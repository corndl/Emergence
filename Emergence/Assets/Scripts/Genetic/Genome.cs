using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Genome {

	List<IGene> listGene = new List<IGene>();

	// mutate sur la liste des genes
	void Mutate () {
		foreach (IGene leGene in listGene){
			leGene.Mutate();
		}
	}

	//add gene
	void add(IGene leGene){
		listGene.Add(leGene);
	}

}
