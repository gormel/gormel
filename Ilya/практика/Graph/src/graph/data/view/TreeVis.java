/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package graph.data.view;

import graph.data.Tree;
import graph.data.TreeNode;
import java.awt.Graphics;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 *
 * @author Илья
 */
public class TreeVis {
    private Map<TreeNode, TreeNodeVis> nodeStore = new HashMap<>();
    private Tree g;

    public TreeVis(Tree g) {
        this.g = g;
        refreshNodes();
    }
    
    public final void refreshNodes() {
        for (TreeNode node : g.getNodes()) {
            if (!nodeStore.containsKey(node)) {
                TreeNodeVis vis = new TreeNodeVis(node, this);
                nodeStore.put(node, vis);
            }
        }
        
        List<TreeNode> toDelete = new ArrayList<>();
        for (TreeNode node : nodeStore.keySet()) {
            if (!g.getNodes().contains(node)) {
                toDelete.add(node);
            }
        }
        
        for(TreeNode del : toDelete)
        {
            nodeStore.remove(del);
        }
    }
    
    public void GraphPaint(Graphics grph){
        for (TreeNodeVis vis : nodeStore.values()){
            vis.Draw(grph);
        }
    }
    
    public TreeNodeVis getVisualisation(TreeNode node) {
        return nodeStore.get(node);
    }
}
