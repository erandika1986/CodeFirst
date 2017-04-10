using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CF.Business.Common
{
    public class EmailConstant
    {
        public const string PassowrdResetEmail = "<table style='width:100%;border:1px solid black;margin:0px;padding:0px;border-collapse:collapse'>" +
"<tbody>" +
    "<tr style='width:100%;margin:0px;padding:0px;border:0px;'>" +
        "<td style='padding-left:100px;padding-bottom:10px;padding-top:10px;background-color:#808080;border:none;outline:none'>" +
            "<label style='text-align:left;font-size:large;color:white'><b>SLIA</b></label>" +
        "</td>" +
    "</tr>" +
    "<tr style='width:100%;margin:0px;padding:0px;border:0px;background-color:#53ba5b'>" +
        "<td style='background-color:#53ba5b;padding-left:10px;padding-bottom:10px;padding-top:10px;border:none;outline:none'>" +
            "<label style='text-align:center;font-size:xx-large;color:black;'><b>Password Reset Information</b></label>" +
        "</td>" +
    "</tr>" +
    "<tr style='width:100%;margin:0px;padding:0px;border:0px;background-color:#ff0000;'>" +
        "<td style='background-color:#ff0000;padding-left:10px;padding-bottom:10px;padding-top:10px;border:none;outline:none'>" +
            "<label style='text-align:center;font-size:medium;color:white;'>This link is valid for ONE USE ONLY.</label>" +
        "</td>" +
    "</tr>" +
    "<tr style='width:100%;margin:0px;padding:0px;border:0px;'>" +
        "<td style='background-color:none;padding-left:10px;padding-bottom:10px;padding-top:10px;'>" +
           " <label style='text-align:center;font-size:medium;color:black;'>Click the button below to reset your password.</label>" +
        "</td>" +
    "</tr>" +
    "<tr style='width:100%;margin:0px;padding:0px;border:0px;'>" +
        "<td style='background-color:none;padding-left:10px;padding-bottom:10px;padding-top:10px;'>" +
            "<a href='{0}' style='color:#ffffff;text-decoration:none;background-color:#008a32;border-top:15px solid #008a32;border-bottom:15px solid #008a32;border-left:15px solid #008a32;border-right:15px solid #008a32;display:inline-block' target='_blank'><font face='Walsheim-Medium', Arial, sans-serif' color='#ffffff' style='font-size:18px;line-height:20px'><span class='il'>RESET</span> YOUR <span class='il'>PASSWORD</span></font></a>" +
        "</td>" +
    "</tr>" +
    "<tr style='width:100%;margin:0px;padding:0px;border:0px;'>" +
        "<td style='padding-top:40px;padding-bottom:40px;padding-left:100px;padding-right:20px' align='left'>" +
            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>" +
                "To <span class='il'>reset</span> your <span class='il'>password</span>:<br>" +
            "</font>" +
            "<table width='100%' border='0' cellspacing='0' cellpadding='0'>" +
                "<tbody>" +
                    "<tr>" +
                        "<td width='3%' valign='top' style='padding-top:10px;padding-right:5px'>" +
                            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>1.</font>" +
                        "</td>" +
                        "<td width='97%' valign='top' style='padding-top:10px'>" +
                            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>Enter your username.</font>" +
                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td valign='top' style='padding-top:10px;padding-right:5px'>" +
                            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>2.</font>" +
                        "</td>" +
                        "<td valign='top' style='padding-top:10px'>" +
                            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>Enter your new<span class='il'>password</span>.</font>" +
                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td valign='top' style='padding-top:10px;padding-right:5px'>" +
                           " <font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>3.</font>" +
                        "<td valign='top' style='padding-top:10px'>" +
                        "</td>" +
                            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>Confirm your new<span class='il'>password</span>.</font>" +
                        "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td valign='top' style='padding-top:10px;padding-right:5px'>" +
                            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>4.</font>" +
                        "</td>" +
                        "<td valign='top' style='padding-top:10px'>" +
                            "<font face='Arial, sans-serif' color='#333333' style='font-size:14px;line-height:20px'>Click 'Submit'.</font>" +
                        "</td>" +
                    "</tr>" +
                "</tbody>" +
            "</table>" +

        "</td>" +
    "</tr>" +
"</tbody>" +
"</table>";
    }
}
