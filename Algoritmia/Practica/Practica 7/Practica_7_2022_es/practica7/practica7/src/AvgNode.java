import java.util.ArrayList;
import java.util.UUID;

/**
 * 
 */

/**
 * @author UO285176
 *
 */
public class AvgNode extends Node {

	private Imagen[] dataset;
	private ArrayList<Integer> sol;
	private Imagen half1_avg, half2_avg;

	public AvgNode(Imagen[] imagenes, int grupo, ArrayList<Integer> imagenesNodo) {
		ID = UUID.randomUUID();
		this.dataset = imagenes;// copia de todas las imágenes
		this.depth = grupo;
		this.sol = new ArrayList<Integer>(imagenesNodo);
		calculateHeuristicValue();
	}

	@Override
	public void calculateHeuristicValue() {
		if (!isSolution()) {
			this.heuristicValue = -2;
		} else {
			Imagen group1_pd = new Imagen(dataset[0].getWidth(), dataset[0].getHeight());
			Imagen group2_pd = new Imagen(dataset[0].getWidth(), dataset[0].getHeight());
			for (int i = 0; i < this.sol.size(); i++) {
				if (sol.get(i) == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (sol.get(i) == 2)
					group2_pd.addSignal(this.dataset[i]);
			}
			this.heuristicValue = (group2_pd.zncc(group1_pd)) * (-1);
			this.half1_avg = group1_pd.copy();
			this.half2_avg = group2_pd.copy();
		}

	}

	@Override
	public ArrayList<Node> expand() {
		// x3 new AvgNode, que imagen va a que conjunto
		ArrayList<Node> res = new ArrayList<Node>();
		sol.add(0);
		res.add(new AvgNode(dataset, depth+1, sol));
		sol.set(depth, 1);
		res.add(new AvgNode(dataset, depth+1, sol));
		sol.set(depth, 2);
		res.add(new AvgNode(dataset, depth+1, sol));
		return res;
	}

	@Override
	public boolean isSolution() {
		// devuelve si es un nodo hoja o no
		return getDepth() == this.dataset.length;

	}

	public Imagen getHalf2_avg() {
		return half2_avg;
	}

	public Imagen getHalf1_avg() {
		return half1_avg;
	}

}
