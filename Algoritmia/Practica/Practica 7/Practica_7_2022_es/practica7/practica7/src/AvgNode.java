import java.util.ArrayList;
import java.util.Random;

/**
 * 
 */

/**
 * @author UO285176
 *
 */
public class AvgNode extends Node {

	private Imagen[] dataset;
	private int grupo;
	private int nivelSol;
	private ArrayList<Imagen> imagenesNodo;

	public AvgNode(Imagen[] imagenes, int grupo, ArrayList<Imagen> imagenesNodo) {
		this.dataset = imagenes;// copia de todas las imágenes
		this.depth = imagenesNodo.size();// profundidad se corresponde con la longitud del array de imágenes del nodo
		this.nivelSol = imagenes.length;// profundidad solución
		this.grupo = grupo;
		this.imagenesNodo = imagenesNodo;
		calculateHeuristicValue();
	}

	@Override
	public void calculateHeuristicValue() {
		if (!isSolution()) {
			this.heuristicValue = -2;
		} else {
			Random r = new Random();
			Imagen group1_pd = new Imagen(dataset[0].getWidth(), dataset[0].getHeight());
			Imagen group2_pd = new Imagen(dataset[0].getWidth(), dataset[0].getHeight());
			for (int i = 0; i < nivelSol; i++) {
				this.grupo = r.nextInt(3);
				if (this.grupo == 1)
					group1_pd.addSignal(this.dataset[i]);
				if (this.grupo == 2)
					group2_pd.addSignal(this.dataset[i]);
			}
			this.heuristicValue = group1_pd.zncc(group2_pd) * -1;
		}

	}

	@Override
	public ArrayList<Node> expand() {
		// x3 new AvgNode, que imagen va a que conjunto
		ArrayList<Node> res = new ArrayList<>();
		res.add(new AvgNode(this.dataset, 0, new ArrayList<Imagen>(imagenesNodo)));
		res.add(new AvgNode(this.dataset, 1, new ArrayList<Imagen>(imagenesNodo)));
		res.add(new AvgNode(this.dataset, 2, new ArrayList<Imagen>(imagenesNodo)));
		return res;
	}

	@Override
	public boolean isSolution() {
		// devuelve si es un nodo hoja o no
		return getDepth() == this.nivelSol ? true : false;

	}

}
