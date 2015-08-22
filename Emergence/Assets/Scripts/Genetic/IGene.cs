using UnityEngine;
using System.Collections;

public interface IGene {

	// Chargement des genes
	void set(string sgene);
	// save gene
	string get ();
	// children
	IGene children();

	// type gene (int, list)
	// range
	// % chance de mutation


}
