using Script.GlobalsScript;

public static class UnionFind
{
    // Find, assign and return the root id of a node, assign every parent of the node to this id (path compressed)
    public static int Find(Subset[] subset, int i)
    {
        if (subset[i].Parent != i)
            subset[i].Parent = Find(subset, subset[i].Parent);

        return subset[i].Parent;
    }

    // Create union between 2 subset if it's not a cycle
    public static void Union(Subset[] subset, int x, int y)
    {
        // Find root of both node and compressed the path
        int xRoot = Find(subset, x);
        int yRoot = Find(subset, y);
         
        // Create Union between
        if (subset[xRoot].rank > subset[yRoot].rank)
        {
            subset[yRoot].Parent = x;
        } else if (subset[xRoot].rank > subset[yRoot].rank)

        {
            subset[xRoot].Parent = yRoot;
        }
        else
        {
            {
                subset[yRoot].Parent = xRoot;
                subset[xRoot].rank++;
            }
        }
    }
}
