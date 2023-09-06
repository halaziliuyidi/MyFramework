UIFramework使用方法

制作：
#
制作规范按照独立界面进行制作
例如：登陆界面，主界面，商店界面，成就界面等
制作好后将其拖进Resources文件夹下的UIPanel文件夹
在UIFramework/Resources/UIPanelType中添加新增的界面的类型和路径

初始化：
#
UIRoot挂载在UI根节点的物体上
Start方法会自动加载Type为MainMenu的界面
#

跳转：
UIManager.Instance.PushPanel(panelType);

返回主界面：
UIManager.Instance.PopPanel();
