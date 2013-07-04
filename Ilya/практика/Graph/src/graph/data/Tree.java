package graph.data;


import java.util.ArrayList;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import sun.misc.Queue;

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
	
	List<TreeNode> children = new ArrayList<>();
	children.addAll(node.getChildren());
	for (TreeNode child : children) {
	    RemoveNode(child);
	}
    }
    
    public List<TreeNode> find(int value) {
	List<TreeNode> found = new ArrayList<>();
	
	Queue queue = new Queue();
	queue.enqueue(head);
	
	while (!queue.isEmpty()) {
	    try {
		TreeNode node = (TreeNode) queue.dequeue();
		for (TreeNode n : node.getChildren()) {
		    queue.enqueue(n);
		}
		
		if (node.getValue() == value)
		    found.add(node);
	    } catch (InterruptedException ex) {
		throw new RuntimeException(ex.getMessage());
	    }
	}
	
	return found;
    }
    
    public List<TreeNode> getMinMax(boolean max) {
	List<TreeNode> found = new ArrayList<>();
	TreeNode fakeNode = new TreeNode();
	fakeNode.setValue(max ? Integer.MIN_VALUE : Integer.MAX_VALUE);
	found.add(fakeNode);
	
	Queue queue = new Queue();
	queue.enqueue(head);
	
	while (!queue.isEmpty()) {
	    try {
		TreeNode node = (TreeNode) queue.dequeue();
		for (TreeNode n : node.getChildren()) {
		    queue.enqueue(n);
		}
		
		if (node.getValue() == found.get(0).getValue()) {
		    found.add(node);
		    continue;
		}
		if (node.getValue() < found.get(0).getValue() ^ max) {
		    found.clear();
		    found.add(node);
		}
		
	    } catch (InterruptedException ex) {
		throw new RuntimeException(ex.getMessage());
	    }
	}
	return found;
    }

    public TreeNode getHead() {
        return head;
    }
    
}
