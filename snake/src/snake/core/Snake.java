/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package snake.core;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Deque;
import java.util.List;
import java.util.Queue;
import java.util.concurrent.DelayQueue;
import snake.utils.Vector;

/**
 *
 * @author User
 */
public class Snake {
    Deque<Vector> turns = new ArrayDeque<Vector>();
    
    Vector vel;
    Vector pos;
    
    public void up() {
        turns.add(pos);
        vel = new Vector(0, -1);
    }
    
    public void left() {
        turns.add(pos);
        vel = new Vector(-1, 0);
    }
    
    public void down() {
        turns.add(pos);
        vel = new Vector(0, 1);
    }
    
    public void right() {
        turns.add(pos);
        vel = new Vector(1, 0);
    }
    
    private void checkTurns() {
        Vector last = turns.pollLast();
        Vector subLast = turns.peekLast();
        if (last.equals(subLast)) {
            checkTurns();
        } else {
            turns.addLast(last);
        }
    }
    
    public void update() {
        Vector first = turns.pollFirst();
        turns.addFirst(first.add(vel));
        
        Vector last = turns.pollLast();
        Vector lastVel = turns.peekLast().sub(last).norm();
        turns.addLast(last.add(lastVel));
    }
}
