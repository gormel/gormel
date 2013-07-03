package graph.data;


import java.util.ArrayList;
import java.util.List;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Илья
 */
public class Tree {
    TreeNode head;
    private List<TreeNode> nodes = new ArrayList<>();

    public Tree(TreeNode head) {
        this.head = head;
        nodes.add(head);
    }
    
    public List<TreeNode> getNodes() {
        return nodes;
    }
    
    public void AddNode(TreeNode node) {
        nodes.add(node);
    }
    
    public void RemoveNode(TreeNode node)
    {
        for (TreeNode parent : nodes)
        {
            if (parent.getChildren().contains(node))
            {
                parent.getChildren().remove(node);
                break;
            }
        }
        
        nodes.remove(node);

    }

    public TreeNode getHead() {
        return head;
    }
    
}
