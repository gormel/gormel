/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package snake.core;

import java.util.ArrayDeque;
import java.util.Deque;
import java.util.Queue;
import snake.utils.Vector;

/**
 *
 * @author User
 */
public class Snake {
    Deque<Vector> turns = new ArrayDeque<>();
    
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
    
    public Queue<Vector> getTurns() {
        return turns;
    }
    
    public void right() {
        turns.add(pos);
        vel = new Vector(1, 0);
    }
        
    public void update() {
        Vector first = turns.pollFirst();
        turns.addFirst(first.add(vel));
        
        Vector last = turns.pollLast();
        Vector lastVel = turns.peekLast().sub(last).norm();
        Vector newLast = last.add(lastVel);
        if (!last.equals(newLast)) {
            turns.addLast(newLast);
        }
    }
    
    public void upgrade(int times) {
        Vector last = turns.pollLast();
        Vector lastVel = turns.peekLast().sub(last).norm().mul(times);
        turns.addLast(last.add(lastVel));
    }
    
    public void upgrade() {
        upgrade(1);
    }
}
