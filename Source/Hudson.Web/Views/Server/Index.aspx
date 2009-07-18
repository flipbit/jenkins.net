<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    
    <title>Hudson Monitor</title>
    
    <style type="text/css" media="all">@import "/css/hudson.css";</style>	
       
    
</head>
<body>
    <div class="container">        
        
        <h1>
            Hudson Monitor
        </h1>
        
        <form method="post" action="/server/connect">
        
            <fieldset>
        
                <p class="info-text">
                    <% if (Session["error"] == null) { %>
                        Enter your <a href="https://hudson.dev.java.net/">Hudson</a> server details below:
                    <% } else { %>
                        <span class="error-text"><%= Session["error"] %></span>
                    <% } %>
                </p>
        
                <p>
                    <label for="url">URL:</label> 
                    <input type="text" name="url" />
                </p>
                
                <p>
                    <label for="username">Username:</label>
                    <input type="text" name="username" />
                </p>
                
                <p>            
                    <label for="password">Password:</label>
                    <input type="password" name="password" />
                </p>
                
                <p>
                    <input type="submit" value="OK" class="btn" />
                </p>
            
            </fieldset>
            
        </form>   
    </div>
</body>
</html>
