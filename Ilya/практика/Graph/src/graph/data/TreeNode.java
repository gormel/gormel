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
public class TreeNode {
    private List<TreeNode> children = new ArrayList<>();
    
    public TreeNode() {
        
    }
    
    public List<TreeNode> getChildren() {
        return children;
    }
}
