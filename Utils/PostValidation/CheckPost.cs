using System;
using Server.Models.PostModels;

namespace Server.Utils.PostValidation
{
    public class CheckPost
    {
        public static bool IsPostValid(Post post)
        {
            if (post.Text.Length > 0 && post.Title.Length is <= 50 and > 0)
                return true;
            
            return false;
        }
        
        public static bool IsCommentValid(PostComment comment)
        {
            if (comment.Text is null)
                return false;
                
            
            return true;
        }
        
    }
}