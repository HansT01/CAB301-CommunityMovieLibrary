// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 



	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
		return count == 0;
	}

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		if (movie == null) return false;

		if (root == null)
        {
			root = new BTreeNode(movie);
			count++;
			return true;
        }

		if (InsertDFS(root, movie))
        {
			count++;
			return true;
        }
		return false;
	}

	private bool InsertDFS(BTreeNode node, IMovie movie)
    {
		int comparison = movie.CompareTo(node.Movie);
		if (comparison < 0)
        {
			if (node.LChild == null)
            {
				node.LChild = new BTreeNode(movie);
				return true;
            }
			return InsertDFS(node.LChild, movie);
        }
		else if (comparison > 0)
        {
			if (node.RChild == null) 
			{ 
				node.RChild = new BTreeNode(movie);
				return true;
			}
			return InsertDFS(node.RChild, movie);
        }
		else
        {
			return false;
        }
    }



	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		if (movie == null) return false;

		Tuple<BTreeNode, BTreeNode> dfsResult = BTreeDFS(null, root, movie);
		BTreeNode parent = dfsResult.Item1;
		BTreeNode node = dfsResult.Item2;
		// no matching node is found
		if (node == null)
        {
			return false;
        }

		// from QUT lecture slides
		if (node.LChild != null && node.RChild != null)
        {
			// This version will always prioritize going to the left child, then down the right branch
			if (node.LChild.RChild == null)
            {
				node.Movie = node.LChild.Movie;
				node.LChild = node.LChild.LChild;
            }
			else
			{
				BTreeNode p = node.LChild;
				BTreeNode pp = node;

				while (p.RChild != null)
                {
					pp = p;
					p = p.RChild;
                }

				node.Movie = p.Movie;
				pp.RChild = p.LChild;
			}
        }
		else
        {
			BTreeNode c;
			if (node.LChild != null)
            {
				c = node.LChild;
            }
			else
            {
				c = node.RChild;
            }

			if (node == root)
            {
				root = c;
            }
			else
            {
				if (node == parent.LChild)
                {
					parent.LChild = c;
                }
				else
                {
					parent.RChild = c;
                }
            }
        }
		count--;
		return true;
	}

	// Returns the parent node as the first element, then the child as the second.
	public Tuple<BTreeNode, BTreeNode> BTreeDFS(BTreeNode parent, BTreeNode node, IMovie movie)
    {
		if (node == null)
        {
			return new Tuple<BTreeNode, BTreeNode>(parent, node);
		}
		int comparison = movie.CompareTo(node.Movie);
		if (comparison < 0)
        {
			// movie is lesser than node
			return BTreeDFS(node, node.LChild, movie);
        }
		else if (comparison > 0)
        {
			// movie is higher than node
			return BTreeDFS(node, node.RChild, movie);
        }
		else
        {
			// movie is same as node
			return new Tuple<BTreeNode, BTreeNode>(parent, node);
        }
    }


	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		if (movie == null) return false;

		return (BTreeDFS(null, root, movie).Item2 != null);
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		if (movietitle == null) return null;

		Movie target = new Movie(movietitle);
		BTreeNode node = BTreeDFS(null, root, target).Item2;
		return (node != null) ? node.Movie : null;
	}


	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
		IMovie[] movies = new IMovie[count];
		int index = 0;
		InorderDFS(root, movies, ref index);
		return movies;
	}

	private void InorderDFS(BTreeNode node, IMovie[] movies, ref int index)
    {
		if (node == null)
        {
			return;
        }
		InorderDFS(node.LChild, movies, ref index);
		movies[index] = node.Movie;
		index++;
		InorderDFS(node.RChild, movies, ref index);
    }


	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		root = null;
		count = 0;
	}
}





