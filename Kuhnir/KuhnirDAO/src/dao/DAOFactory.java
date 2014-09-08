package dao;

public class DAOFactory {
	
	public static PlayerDAO getFilePlayerDAO() {
		return FilePlayerDAO.getInstance();
	}
}
