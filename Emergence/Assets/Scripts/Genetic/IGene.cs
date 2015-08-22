﻿using UnityEngine;
using System.Collections;

public interface IGene {

	// Chargement des genes
	//void set(string sgene);
	// serialize gene
	//string get ();

	// children
	IGene children();

	void Mutate();

	// type gene (int, list)
	// range
	// % chance de mutation


}
